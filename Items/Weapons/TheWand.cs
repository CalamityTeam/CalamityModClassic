using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class TheWand : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Wand");
			// Tooltip.SetDefault("The ultimate wand");
		}

		public override void SetDefaults()
		{
			Item.width = 40;
			Item.damage = 1;
			Item.mana = 500;
			Item.DamageType = DamageClass.Magic;
			Item.noMelee = true;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.useTurn = true;
			Item.useStyle = 1;
			Item.knockBack = 0f;
			Item.UseSound = SoundID.Item102;
			Item.autoReuse = true;
			Item.height = 36;
            Item.value = Item.buyPrice(2, 50, 0, 0);
            Item.rare = 10;
            Item.shoot = Mod.Find<ModProjectile>("SparkInfernal").Type;
			Item.shootSpeed = 24f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 15;
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
	        if (Main.rand.Next(3) == 0)
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 6);
	        }
	    }
	}
}
