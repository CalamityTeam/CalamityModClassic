using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons 
{
	public class BrimlashBuster : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Brimlash Buster");
			//Tooltip.SetDefault("50% chance to do triple damage on enemy hits");
		}

		public override void SetDefaults()
		{
			Item.width = 68;
			Item.damage = 120;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 25;
			Item.useTime = 25;
			Item.useTurn = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 8;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 68;
			Item.value = 3000000;
			Item.rare = ItemRarityID.Cyan;
			Item.shoot = Mod.Find<ModProjectile>("Brimlash").Type;
			Item.shootSpeed = 18f;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "Brimlash");
	        recipe.AddIngredient(null, "CoreofChaos", 3);
	        recipe.AddIngredient(ItemID.FragmentSolar, 10);
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
		}
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.NextBool(3))
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.LifeDrain);
	        }
	    }
	    
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
	    	target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 300);
	    	if (Main.rand.NextBool(3))
	    	{
	    		Item.damage = 360;
	    	}
	    	else
	    	{
	    		Item.damage = 120;
	    	}
		}
	}
}
