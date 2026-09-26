using System.Linq;
using UnityEngine;

public class PinCanvas : MonoBehaviour
{
    public void SetPin()
    {
        if (Pin.SelectedPin != null && PlayerSceneSetup.instance != null)
        {
            var itemsType = PlayerSceneSetup.instance.ItemsType;

            if (itemsType != null && itemsType.Count > 0)
            {
                var firstItem = itemsType[0];

                string pinType = firstItem.Keys.First();

                Debug.Log(pinType);

                Pin.SelectedPin.OnPinChange(pinType);

                itemsType.RemoveAt(0);

                PlayerSceneSetup.PinsToSetup = false;
            }
            else
            {
                Debug.LogWarning("No hay elementos en ItemsType para configurar el pin.");
            }
        }
        else
        {
            Debug.LogWarning("Falta seleccionar un Pin o la instancia de PlayerSceneSetup no está disponible.");
        }
    }
}