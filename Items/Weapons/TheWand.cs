using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons 
{
	public class TheWand : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("The Wand");
			//Tooltip.SetDefault("The ultimate wand");
		}

		public override void SetDefaults()
		{
			Item.width = 40;  //The width of the .png file in pixels divided by 2.
			Item.damage = 1;  //Keep this reasonable please.
			Item.mana = 500;
			Item.DamageType = DamageClass.Magic;  //Dictates whether this is a melee-class weapon.
			Item.noMelee = true;
			Item.useAnimation = 20;
			Item.useTime = 20;  //Ranges from 1 to 55.
			Item.useTurn = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 0f;  //Ranges from 1 to 9.
			Item.UseSound = SoundID.Item102;
			Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
			Item.height = 36;  //The height of the .png file in pixels divided by 2.
			Item.value = 30000000;  //Value is calculated in copper coins.
			Item.shoot = Mod.Find<ModProjectile>("SparkInfernal").Type;
			Item.shootSpeed = 24f;
		}
		
		public override void ModifyTooltips(List<TooltipLine> list)
		{
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(108, 45, 199);
	            }
	        }
	    }
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.WandofSparking);
			recipe.AddIngredient(null, "HellcasterFragment", 5);
	        recipe.AddTile(null, "DraedonsForge");
	        recipe.Register();
		}
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.NextBool(3))
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Torch);
	        }
	    }
	}
}
