using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons 
{
	public class AbyssBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Abyss Blade");
			//Tooltip.SetDefault("Hitting enemies will cause the crush depth debuff\nThe lower the enemies' defense the more damage they take from this debuff");
		}

		public override void SetDefaults()
		{
			Item.width = 44;
			Item.damage = 76;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 17;
			Item.useTime = 17;
			Item.useTurn = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 8f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 54;
			Item.value = 900000;
			Item.rare = ItemRarityID.Yellow;
			Item.shoot = Mod.Find<ModProjectile>("DepthOrb").Type;
			Item.shootSpeed = 19f;
		}
		
		public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "DepthBlade");
	        recipe.AddIngredient(ItemID.BrokenHeroSword);
	        recipe.AddIngredient(null, "CoreofEleum", 5);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.NextBool(3))
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Water);
	        }
	    }
	    
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
	    	target.AddBuff(Mod.Find<ModBuff>("CrushDepth").Type, 300);
		}
	}
}
