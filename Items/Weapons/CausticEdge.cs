using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons 
{
	public class CausticEdge : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Caustic Edge");
			//Tooltip.SetDefault("Give Sick");
		}

		public override void SetDefaults()
		{
			Item.width = 42;
			Item.damage = 40;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 27;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 27;
			Item.knockBack = 5;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			Item.height = 48;
			Item.value = 160000;
			Item.rare = ItemRarityID.Orange;
		}
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.BladeofGrass);
			recipe.AddIngredient(ItemID.LavaBucket);
			recipe.AddIngredient(ItemID.Deathweed, 5);
	        recipe.AddTile(TileID.DemonAltar);	
	        recipe.Register();
		}
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.NextBool(5))
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.GreenFairy);
	        }
	    }
	    
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(BuffID.Poisoned, 480);
		}
	}
}
