using StarterAssets;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public Transform weaponHolder;
    public Transform[] weapons;
    public float switchTime;

    private int selectedWeapon;
    private float timeSinceLastSwitch;
    private StarterAssetsInputs input;

    public Animator animator;

    private void Start()
    {
        input = GetComponent<StarterAssetsInputs>();

        SetWeapons();
        Select(selectedWeapon);

        timeSinceLastSwitch = 0f;
    }

    private void SetWeapons()
    {
        weapons = new Transform[weaponHolder.childCount];

        for (int i = 0; i < weaponHolder.childCount; i++)
            weapons[i] = weaponHolder.GetChild(i);
    }

    private void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        if (input.weapon1)
        {
            selectedWeapon = 0;
            input.weapon1 = false;
            animator.SetBool("holdingItem", false);
            
        }
        if (input.weapon2)
        {
            selectedWeapon = 1;
            input.weapon2 = false;
            animator.SetBool("holdingItem", true);
        }

        if (previousSelectedWeapon != selectedWeapon) Select(selectedWeapon);

        timeSinceLastSwitch += Time.deltaTime;
    }

    private void Select(int weaponIndex)
    {
        for (int i = 0; i < weapons.Length; i++)
            weapons[i].gameObject.SetActive(i == weaponIndex);

        timeSinceLastSwitch = 0f;
    }
}