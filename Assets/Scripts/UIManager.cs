using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor;

public class UIManager : MonoBehaviour
    {
   
        public TextMeshProUGUI instructionText;
        public Button continueButton;

        private int step = 0;

        void Start()
        {
            continueButton.onClick.AddListener(NextStep);
        }


        void NextStep()
        {
            step++;

            switch (step)
            {
                case 1:
                    instructionText.text = "To move, use the left joystick.";
                    break;
                case 2:
                    instructionText.text = "To grab an object, press the grip button.";
                    break;
                case 3:
                    instructionText.text = "You're ready! Enjoy the game!";
                    continueButton.gameObject.SetActive(false); // Hide button after the last step
                    break;
            }
        }
}
