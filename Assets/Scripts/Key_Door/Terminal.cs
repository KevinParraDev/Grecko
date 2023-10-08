using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : InteractableObject
{
     [SerializeField]
     private GameObject _objectToActive;
     private IActivable _activable;

     [SerializeField]
     private Sprite _unlockSprite;

     private bool _keyUnlock = false;

     private void Start()
     {
          if (_objectToActive.TryGetComponent(out IActivable act))
               _activable = act;
     }
     public override void Interact()
     {
          if(_keyUnlock)
          {
               _activable?.Activate();
               ChangeSprite(_unlockSprite);
               _keyUnlock = false;
          }
     }

    public void KeyUnlock()
     {
          _keyUnlock = true;
     }

     public void ChangeSprite(Sprite newSprite) 
     {
          if(TryGetComponent(out SpriteRenderer spriteRenderer))
          {
               spriteRenderer.sprite = newSprite;
          }
     }
}