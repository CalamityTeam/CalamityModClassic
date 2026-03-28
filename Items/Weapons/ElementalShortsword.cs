using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class ElementalShortsword : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Elemental Shiv");
			// Tooltip.SetDefault("Don't underestimate the power of shivs");
		}

		public override void SetDefaults()
		{
			Item.useStyle = 3;
			Item.useTurn = false;
			Item.useAnimation = 10;
			Item.useTime = 10;
			Item.width = 44;
			Item.height = 44;
			Item.damage = 140;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.knockBack = 8.5f;
			Item.UseSound = SoundID.Item1;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("ElementBallShiv").Type;
			Item.shootSpeed = 14f;
            Item.value = Item.buyPrice(1, 20, 0, 0);
            Item.rare = 10;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "TerraShiv");
			recipe.AddIngredient(null, "GalacticaSingularity", 5);
			recipe.AddIngredient(ItemID.LunarBar, 5);
	        recipe.AddTile(TileID.LunarCraftingStation);	
	        recipe.Register();
		}
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.Next(5) == 0)
			{
				int num250 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 66, (float)(player.direction * 2), 0f, 150, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1.3f);
				Main.dust[num250].velocity *= 0.2f;
				Main.dust[num250].noGravity = true;
			}
	    }
	    
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
	    	target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 500);
	    	target.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 500);
	    	target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 500);
	    	target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 500);
		}
	}
}
