using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons
{
	public class PurityAxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Axe of Purity");
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
	        Item.useStyle = ItemUseStyleID.Swing;
	        Item.knockBack = 7.5f;
	        Item.value = 300000;
	        Item.rare = ItemRarityID.Pink;
	        Item.UseSound = SoundID.Item1;
	        Item.autoReuse = true;
	    }
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "FellerofEvergreens");
	        recipe.AddIngredient(ItemID.PixieDust, 10);
	        recipe.AddIngredient(ItemID.CrystalShard, 5);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
	    }
	    
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.NextBool(5))
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Enchanted_Pink);
	        }
	    }
	}
}