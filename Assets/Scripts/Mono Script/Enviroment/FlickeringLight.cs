using System.Collections;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    [SerializeField] private Light spotLight;
    [SerializeField] private Light areaLight;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FlickerLight());
    }

    IEnumerator FlickerLight()
    {
        while (true)
        {
            var randomNum = Random.Range(0, 20);

            if (randomNum % 7 == 0)
                SwitchOff();
            else
                SwitchOn();

            yield return new WaitForSeconds(0.2f);
        }
    }

    private void SwitchOn()
    {
        spotLight.intensity = 500f;
        spotLight.range = 10f;
        areaLight.intensity = 100f;
        areaLight.range = 8f;
    }

    private void SwitchOff()
    {
        spotLight.intensity = 10f;
        spotLight.range = 2.5f;
        areaLight.intensity = 10f;
        areaLight.range = 2.5f;
    }
}
