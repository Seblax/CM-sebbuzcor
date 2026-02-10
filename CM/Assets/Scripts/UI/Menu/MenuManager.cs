using System.Collections.Generic;
using UnityEngine;

public class MenuManager : Singleton<MenuManager>
{
    //La lista de menus qu ehay en la escena
    [SerializeField]
    public List<Menu> menuList = new List<Menu>();

    private void Start()
    {
        //Mostramos men� que toque
        if (menuList.Count > 0)
            ShowMenu(menuList[0]);
    }

    /// <summary>
    /// Sirve para activar o desactivar los menus en funci�n al menu que se est� mostrando
    /// </summary>
    /// <param name="menuToShow"></param>
    public void ShowMenu(Menu menuToShow)
    {
        //Nos Seguramos de que exista dicho menu
        if (!menuList.Contains(menuToShow))
        {
            Debug.LogErrorFormat($"El menu {menuToShow} no forma parte de la lista de menus");
        }

        //Por cada menu en la lista aplicaremos lo siguiente
        foreach (Menu menu in menuList)
        {
            if (menu == menuToShow)                    //Si el menu es igual al menu que toca estar activado, entonces se activar� y se llamara al evento menuDidAppear
            {
                menu.gameObject.SetActive(true);
                menu.appear.Invoke();
            }
            else
            {                                           //Si el menu no es igual al menu que toca estar activado, entonces se desactivar� y se llamara al evento menuWillDisappear                               
                if (menu.gameObject.activeInHierarchy)
                {
                    menu.disappear.Invoke();

                }
            }
        }

    }
}