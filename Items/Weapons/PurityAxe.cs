using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
	public class PurityAxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Axe of Purity");
			// Tooltip.SetDefault("Cleanses the evil");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 43;
	        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
	        Item.width = 58;
	        Item.height = 46;
	        Item.useTime = 19;
	        Item.useAnimation = 19;
	        Item.useTurn = true;
	        Item.axe = 25;
	        Item.useStyle = 1;
	        Item.knockBack = 5f;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
	        Item.UseSound = SoundID.Item1;
	        Item.autoReuse = true;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 12f;
		}
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "FellerofEvergreens");
			recipe.AddIngredient(ItemID.PurificationPowder, 20);
			recipe.AddIngredient(ItemID.PixieDust, 10);
	        recipe.AddIngredient(ItemID.CrystalShard, 5);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
	    }
	    
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.Next(5) == 0)
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 58);
	        }
	    }
	}
}