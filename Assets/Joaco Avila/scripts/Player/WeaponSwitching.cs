using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectWeapon = 0;
    private int weaponCount;

    void Start()
    {
        weaponCount = transform.childCount;
        SelectWeapon();
    }
    void Update()
    {
        int previousSelectedWeapon = selectWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            selectWeapon = (selectWeapon + 1) % weaponCount;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectWeapon <= 0)
            {
                selectWeapon = weaponCount - 1;
            }
            else
            {
                selectWeapon--;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && weaponCount >= 1)
        {
            selectWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && weaponCount >= 2)
        {
            selectWeapon = 1;
        }
        if (previousSelectedWeapon != selectWeapon)
        {
            SelectWeapon();
        }
    }
    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            weapon.gameObject.SetActive(i == selectWeapon);
            i++;
        }
    }
}
