using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons 
{
	public class ExcaliburShortsword : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Excalibur Shortsword");
			//Tooltip.SetDefault("Don't underestimate the power of shortswords");
		}

		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Thrust;
			Item.useTurn = false;
			Item.useAnimation = 10;
			Item.useTime = 10;
			Item.width = 40;
			Item.height = 40;
			Item.damage = 55;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.knockBack = 5.25f;
			Item.UseSound = SoundID.Item1;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.value = 500000;
			Item.rare = ItemRarityID.Pink;
		}
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.HallowedBar, 7);
	        recipe.AddTile(TileID.MythrilAnvil);	
	        recipe.Register();
		}
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.NextBool(5))
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Enchanted_Gold);
	        }
	    }
	}
}
