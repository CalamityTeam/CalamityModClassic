using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons 
{
	public class ForbiddenOathblade : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Forbidden Oathblade");
			//Tooltip.SetDefault("Sword of an ancient demon god");
		}

		public override void SetDefaults()
		{
			Item.width = 70;
			Item.damage = 58;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 25;
			Item.useTime = 25;
			Item.useTurn = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6.5f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 70;
			Item.value = 500000;
			Item.rare = ItemRarityID.LightPurple;
			Item.shoot = Mod.Find<ModProjectile>("Oathblade").Type;
			Item.shootSpeed = 3f;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "BladecrestOathsword");
			recipe.AddIngredient(null, "OldLordOathsword");
			recipe.AddIngredient(ItemID.SoulofFright, 5);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.NextBool(3))
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.ShadowbeamStaff);
	        }
	    }
	    
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
			target.AddBuff(BuffID.ShadowFlame, 240);
			target.AddBuff(BuffID.OnFire, 240);
		}
	}
}
