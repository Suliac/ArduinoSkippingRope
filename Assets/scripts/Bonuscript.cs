using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonuscript : MonoBehaviour {

    // Use this for initialization

    public static bool restart;
    private bool desactiv;

    private Collider collide;

    private MeshRenderer mesh;

	void Start () {

        Debug.Log(restart);
        restart = false;
        desactiv = false;

        collide = GetComponent<BoxCollider>();
        mesh = GetComponent<MeshRenderer>();
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(1,1,0);
        
        

        if (restart && desactiv)
        {
            /*collide.enabled = true;
            mesh.enabled = true;
            */
            gameObject.SetActive(true);
            desactiv = false;
            
        }
        

	}


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            // Destroy(this.gameObject);
            desactiv = true;
            Debug.Log(desactiv);
            gameObject.SetActive(false);

            //collide.SetActive(false);
          /* collide.enabled = false;

            mesh.enabled = false;
            */
            
            
            
            //m_Collider.enabled = !m_Collider.enabled;

        }
    }

}
