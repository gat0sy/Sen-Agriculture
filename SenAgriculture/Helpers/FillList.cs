using AppSenAgriculture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSenAgriculture.Helpers
{
    public class FillList
    {
        BdSenAgricultureContext db = new BdSenAgricultureContext();

        //CHARGEMENT DES UNITÉS DE MESURES        

        public List<ListItem> fillUniteMesure()
        {
            List<ListItem> laListe = new List<ListItem>();
            var liste = db.unitemesures.ToList();
            laListe.Add(new ListItem 
            { 
                Value = "",
                Text = "Selectionner...."
            });
            foreach (var t in liste)
            {
               var item = new ListItem
                {
                    Value = t.IdUnite.ToString(),
                    Text = t.LibelleUnite.ToString()
               };
                laListe.Add(item);
            }
            return laListe;
        }


        
        //CHARGEMENT D'UNE LISTE DE CATÉGORIE
        

        public List<ListItem> fillCategorie()
        {
            List<ListItem> laListe = new List<ListItem>();
            var liste = db.categories.ToList();
            laListe.Add(new ListItem
            {
                Value = null,
                Text = "Selectionner...."
            });
            foreach (var t in liste)
            {
                var item = new ListItem
                {
                    Value = t.IdCategorie.ToString(),
                    Text = t.DescriptionCategorie.ToString()
                };
                laListe.Add(item);
            }
            return laListe;
        }
    }
}
