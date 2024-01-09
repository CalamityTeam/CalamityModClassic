using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons 
{
	public class FlarefrostBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Flarefrost Blade");
		}

		public override void SetDefaults()
		{
			Item.width = 66;
			Item.damage = 55;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 28;
			Item.useTime = 28;
			Item.useTurn = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6.25f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 66;
			Item.maxStack = 1;
			Item.value = 550000;
			Item.rare = ItemRarityID.LightPurple;
			Item.shoot = Mod.Find<ModProjectile>("Flarefrost").Type;
			Item.shootSpeed = 9f;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "CryoBar", 8);
			recipe.AddIngredient(ItemID.HellstoneBar, 8);
			recipe.AddIngredient(ItemID.HallowedBar, 4);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	    	int dustChoice = Main.rand.Next(2);
	    	if (dustChoice == 0)
	    	{
	    		dustChoice = 67;
	    	}
	    	else
	    	{
	    		dustChoice = 6;
	    	}
	        if (Main.rand.NextBool(3))
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, dustChoice);
	        }
	    }
	    
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
			target.AddBuff(BuffID.OnFire, 200);
			target.AddBuff(BuffID.Frostburn, 200);
		}
	}
}
