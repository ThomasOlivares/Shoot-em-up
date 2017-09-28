using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyScript : MonoBehaviour {

    public float energy = 1;
    public Transform maxEnergyBar;
    public Transform energyBar;

    public float speedReload = 1;   // energy by seconds
    private float yPosition = -1.2f;
    private float maxEnergy;
    private Transform maxEnergyTransform;
    private Transform energyTransform;

    public void Start()
    {
        // Create a new shot
        maxEnergyTransform = Instantiate(maxEnergyBar) as Transform;
        energyTransform = Instantiate(energyBar) as Transform;

        // Assign position
        maxEnergyTransform.position = transform.position + new Vector3(0, yPosition, 0);
        energyTransform.position = transform.position + new Vector3(0, yPosition, -1);

        maxEnergyTransform.localScale = new Vector3(0.5f, 0.3f, 0);
        energyTransform.localScale = new Vector3(0.5f, 0.3f, 0);

        //Save maximum hp
        maxEnergy = energy;
    }

    public void Update()
    {
        // Assign position
        maxEnergyTransform.position = transform.position + new Vector3(0, yPosition, 0);
        energyTransform.position = maxEnergyTransform.position + new Vector3(0, 0, -1);

        float hpPercent = (float)energy / (float)maxEnergy;
        energyTransform.localScale = new Vector3(hpPercent * 0.5f, 0.3f, 0);
    }

    public void FixedUpdate()
    {
        if (energy < 1)
        {
            speedReload = 0.75f;
        }
        energy += speedReload * Time.deltaTime;
        if (energy > maxEnergy)
        {
            speedReload = 1;  // return to normal speed reload
            energy = maxEnergy;
        }
    }

    public void Decrement(int used)
    {
        energy -= used;
        if (energy < 0)
        {
            energy = 0;
        }
    }
}
