using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//public abstract class Interactable: MonoBehaviour
//{ 
//    private GameObject interactIcon;
//    [SerializeField]
//    private GameObject interactIconPREFAB;

//    public virtual void Start()
//    {
//        if (interactIconPREFAB != null)
//        {
//            SpriteRenderer ren = GetComponent<SpriteRenderer>();
//            SpriteRenderer renIcon = interactIconPREFAB.GetComponent<SpriteRenderer>();

//            Vector3 position = 
//                new Vector3(transform.position.x, 
//                ren.bounds.center.y + ren.bounds.extents.y + renIcon.bounds.extents.y, 
//                transform.position.y);

//            interactIcon =
//                Instantiate(interactIconPREFAB,
//                position,
//                Quaternion.identity);
//        }

//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (interactIcon == null) return;
//        if (collision.gameObject.tag == "Player")
//        {
//            interactIcon.SetActive(true);
//            interactIcon.GetComponent<Animator>().Play("IconEnter");
//        }
//    }

//    private void OnTriggerExit2D(Collider2D collision)
//    {
//        if (interactIcon == null) return;
//        if (collision.gameObject.tag == "Player")
//            //interactIcon.SetActive(false);
//            interactIcon.GetComponent<Animator>().Play("IconExit");
//    }

//    public abstract void Interact();
//}