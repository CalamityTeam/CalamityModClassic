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
	public class FeralthornClaymore : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Feralthorn Claymore");
		}

		public override void SetDefaults()
		{
			Item.width = 58;
			Item.damage = 63;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 19;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 19;
			Item.useTurn = true;
			Item.knockBack = 7.25f;
			Item.UseSound = SoundID.Item8;
			Item.autoReuse = true;
			Item.height = 58;
			Item.value = 355000;
			Item.rare = ItemRarityID.LightPurple;
			Item.shoot = Mod.Find<ModProjectile>("JungleThorn").Type;
			Item.shootSpeed = 16f;
		}
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "DraedonBar", 12);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.NextBool(4))
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.JungleSpore);
	        }
	    }
	    
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
			target.AddBuff(BuffID.Venom, 200);
		}
	}
}
