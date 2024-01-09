using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons 
{
	public class Lucrecia : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Lucrecia");
			//Tooltip.SetDefault("Finesse\nStriking an enemy with the blade has a 50% chance to make you immune for a short time");
		}

		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Thrust;
			Item.useTurn = false;
			Item.useAnimation = 25;
			Item.useTime = 25;
			Item.width = 58;
			Item.height = 58;
			Item.damage = 72;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.knockBack = 8.25f;
			Item.UseSound = SoundID.Item1;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("DNA").Type;
			Item.shootSpeed = 32f;
			Item.value = 3000000;
			Item.rare = ItemRarityID.Yellow;
		}
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "CoreofCalamity");
			recipe.AddIngredient(null, "BarofLife", 5);
			recipe.AddIngredient(ItemID.SoulofLight, 5);
			recipe.AddIngredient(ItemID.SoulofNight, 5);
	        recipe.AddTile(TileID.MythrilAnvil);	
	        recipe.Register();
		}
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.NextBool(5))
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.BoneTorch);
	        }
	    }
	    
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
	    	if (Main.rand.NextBool(2))
	    	{
				player.immune = true;
				player.immuneTime = 15;
	    	}
		}
	}
}
