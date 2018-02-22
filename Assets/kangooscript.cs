using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kangooscript : MonoBehaviour
{
    public float JumpSpeed = 5.0f;
    public float BigJumpSeed = 20.0f;
    public float DeathY = -10.0f; // Coordonnée en Y à laquelle le joueur meurt
    public float JumpHeighMultiplicator = 10.0f; // On veut sauter plus haut que loin
    public bool EnableMultipleJump = false;
    
    public bool isGrounded = false;
    private bool wannaJump;
    private bool isBigJump;
    private int isGoingRight;
    private Rigidbody rb;

    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Init();
    }

    public void Init()
    {
        wannaJump = false;
        isGoingRight = 1;
        transform.position = new Vector3(0, 2, 0);
        if (rb)
            rb.velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.GetSingleton.CurrentState == GameState.Playing)
        {
            GetInput(); // On récupère les états des controleurs

            if (wannaJump) // Ensuite on test si le joueur souhaite sauter (Big jump ou non)
                Jump();

            if (isDead()) // Si le joueur est mort on le remet en x=0, y=2
                Init();

            Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10); // On colle la caméra au joueur
        }
    }

    /// <summary>
    /// Gestion/récupèration des inputs
    /// </summary>
    void GetInput()
    {
        // Saute
        if ((IsUsingJump() && isGrounded && !EnableMultipleJump) || (EnableMultipleJump && IsUsingJump()))
            wannaJump = true;

        // Detect gros saut : les deux boutons en même temps
        if (IsPressingRightButton() && IsPressingLeftButton() && wannaJump == true)
            isBigJump = true;
        else if (IsPressingRightButton(true) && !IsPressingLeftButton()) // Detect bouton droit
            isGoingRight = 1;
        else if (IsPressingLeftButton(true) && !IsPressingRightButton())// Detect bouton gauche
            isGoingRight = -1;

    }

    /// <summary>
    /// 
    /// On encapsule les 3 manière de récupérer les inputs dans des fonctions a part, comme ça on a juste à changer les inputs ici
    /// plutot que je parcourir tout le code et chercher des "Input.GetButton" partout et en oublier
    /// NB : Par exemple on a besoin de récupérer les input aussi depuis l'écran de victoire
    /// </summary>
    /// <param name="justFirstFrame">Précise si on souhaite recevoir <see cref="true"/> seulement lors du pressage initial de la touche (1fois) ou tout le temps qu'elle est pressée</param>
    /// <returns></returns>
    public bool IsPressingRightButton(bool justFirstFrame = false)
    {
        if (justFirstFrame)
            return Input.GetButtonDown("Fire2");
        else
            return Input.GetButton("Fire2");
    }

    public bool IsPressingLeftButton(bool justFirstFrame = false)
    {
        if (justFirstFrame)
            return Input.GetButtonDown("Fire1");
        else
            return Input.GetButton("Fire1");
    }

    public bool IsUsingJump()
    {
        return Input.GetButtonDown("Jump");
    }


    /// <summary>
    /// Gestion du saut du joueur
    /// </summary>
    void Jump()
    {
        float speed = isBigJump ? BigJumpSeed : JumpSpeed; // Si isBigJumpspeed => speed = BigJumpSeed sinon speed = JumpSpeed
        float jumpHeight = isBigJump ? JumpHeighMultiplicator : 1; // Si bigjump on veut pas de saut plus haut que long sinon on saute sur place avec la faible force de saut

        // gestion des forces
        Vector3 dir = new Vector3(isGoingRight, jumpHeight, 0); // On récupère un vecteur normalisé (Longueur = 1)
        rb.AddForce(dir.normalized * speed, ForceMode.Impulse); // On ajoute cette force au vecteur normalisé multiplié à la vitesse du mode de saut

        // On reset les booléens
        isBigJump = false;
        wannaJump = false;
        isGrounded = false;
    }

    /// <summary>
    /// Test pour savoir si le joueur est "mort"
    /// </summary>
    /// <returns><see cref="true"/> si le joueur est mort et <see cref="false"/> si le joueur est toujours vivant</returns>
    bool isDead()
    {
        if (transform.position.y < DeathY)
            return true;
        else
            return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8 && collision.transform.position.y < transform.position.y)
            isGrounded = true;
    }
}
