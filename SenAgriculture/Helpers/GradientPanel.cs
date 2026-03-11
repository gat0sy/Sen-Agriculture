using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace AppSenAgriculture.Helpers
{
    internal class GradientPanel : Panel
    {
        // Creer une propriété qui defini les coulours pour le dégradé de haut en bas

        public Color gradientTop { get; set; }
        public Color gradientBottom { get; set; }

        // Creer un constructeur pour la classe GradientPanel
        public GradientPanel() {
            // rend le design adaptable au changement de taille.
            this.Resize += GradientPanel_Resize;
        }

        private void GradientPanel_Resize(object sender, EventArgs e)
        {
            this.Invalidate(); //Force un une reconstruction au niveau graphique
        }
        // Écrase la méthode onPaint pour redessiner le degradé d'arrière plan
        protected override void OnPaint(PaintEventArgs e)
        {
            // creer un lineargradient brush avec des valeurs 'top' et 'bottom' pour le degardé de couleur

            LinearGradientBrush linear = new LinearGradientBrush(
                this.ClientRectangle, // Zone à remplir avec le dégradé
                this.gradientTop, // couleur de départ du gradient ( depuis le haut )
                this.gradientBottom, // couleur d'arrivée du gradient ( vers le bas )
                90F // l'angle du dégradé en degrées, ( par example ici 90 ~ vertical )
                );

            // Recupère le contexte graphique pour le dressage.
            Graphics g = e.Graphics;

            // Remplie toute la zone de l'élément avec le dégradé
            g.FillRectangle(linear, this.ClientRectangle);

            // Fait appel de la classe de base onPaint pour s'assurer de la réalisation de tout dressage additionnel
            base.OnPaint(e);
        }
    } 
}
