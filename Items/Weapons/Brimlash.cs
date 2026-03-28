using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class Brimlash : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Brimlash");
		}

		public override void SetDefaults()
		{
			Item.width = 50;
			Item.damage = 64;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 25;
			Item.useTime = 25;
			Item.useTurn = true;
			Item.useStyle = 1;
			Item.knockBack = 6f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 50;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.rare = 7;
			Item.shoot = Mod.Find<ModProjectile>("Brimlash").Type;
			Item.shootSpeed = 15f;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "UnholyCore", 4);
	        recipe.AddIngredient(null, "EssenceofChaos", 3);
	        recipe.AddIngredient(null, "LivingShard", 5);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.Next(3) == 0)
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 235);
	        }
	    }
	    
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
	    	target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 300);
		}
	}
}
