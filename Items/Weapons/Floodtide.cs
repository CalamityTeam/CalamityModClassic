using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons   //where is located
{
    public class Floodtide : ModItem
    {
    	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
		{
			//texture =("CalamityModClassic1Point1/Items/Weapons/Floodtide");
			return true;
		}
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Floodtide");     //Sword name
            Item.damage = 52;            //Sword damage
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;            //if it's melee
            Item.width = 58;              //Sword width
            Item.height = 58;             //Sword height
            ////Tooltip.SetDefault("Launches sharks, because sharks are awesome!");  //Item Description
            Item.useTime = 23;          //how fast 
            Item.useAnimation = 23;  
			Item.useTurn = true;            
            Item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            Item.knockBack = 5.5f;      //Sword knockback
            Item.value = 170000;        
            Item.rare = 6;
            Item.UseSound = SoundID.Item1;       //1 is the sound of the sword
            Item.autoReuse = true;   //if it's capable of autoswing.
            Item.shoot = 408;
            Item.shootSpeed = 11f;                //projectile speed                 
        }
        
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(5) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 217);
            }
        }
        
        public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "VictideBar", 5);
	        recipe.AddIngredient(ItemID.SharkFin, 5);
	        recipe.AddIngredient(ItemID.AdamantiteBar, 5);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	        recipe = CreateRecipe();
	        recipe.AddIngredient(null, "VictideBar", 5);
	        recipe.AddIngredient(ItemID.SharkFin, 5);
	        recipe.AddIngredient(ItemID.TitaniumBar, 5);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
    }
}