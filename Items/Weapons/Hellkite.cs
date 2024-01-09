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
	public class Hellkite : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Hellkite");
			//Tooltip.SetDefault("Contains the power of an ancient drake");
		}

		public override void SetDefaults()
		{
			Item.width = 84;
			Item.damage = 118;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 22;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 22;
			Item.useTurn = true;
			Item.knockBack = 8f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 84;
			Item.value = 500000;
			Item.rare = ItemRarityID.Yellow;
		}
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "DraedonBar", 8);
			recipe.AddIngredient(ItemID.FieryGreatsword);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.NextBool(4))
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.InfernoFork);
	        }
	    }
	    
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
			target.AddBuff(BuffID.OnFire, 1800);
		}
	}
}
