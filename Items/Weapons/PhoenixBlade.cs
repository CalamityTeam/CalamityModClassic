using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
	public class PhoenixBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Phoenix Blade");
			// Tooltip.SetDefault("Enemies explode and emit healing flames on death");
		}

		public override void SetDefaults()
		{
			Item.width = 106;
			Item.damage = 95;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 29;
			Item.useStyle = 1;
			Item.useTime = 29;
			Item.useTurn = true;
			Item.knockBack = 8f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 106;
            Item.value = Item.buyPrice(0, 48, 0, 0);
            Item.rare = 6;
			Item.shootSpeed = 12f;
		}
		
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
			if (target.life <= 0)
			{
				Projectile.NewProjectile(Entity.GetSource_FromThis(null), target.Center.X, target.Center.Y, 0f, 0f, 612, Item.damage, Item.knockBack, Main.myPlayer);
				float spread = 180f * 0.0174f;
				double startAngle = Math.Atan2(Item.shootSpeed, Item.shootSpeed) - spread / 2;
				double deltaAngle = spread / 8f;
				double offsetAngle;
				int i;
				for (i = 0; i < 1; i++ )
				{
					float randomSpeedX = (float)Main.rand.Next(5);
					float randomSpeedY = (float)Main.rand.Next(3, 7);
				   	offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
				   	int projectile1 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), target.Center.X, target.Center.Y, (float)( Math.Sin(offsetAngle) * 5f ), (float)( Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("PhoenixHeal").Type, Item.damage, Item.knockBack, Main.myPlayer);
				    int projectile2 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), target.Center.X, target.Center.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("PhoenixHeal").Type, Item.damage, Item.knockBack, Main.myPlayer);
				    Main.projectile[projectile1].velocity.X = -randomSpeedX;
				    Main.projectile[projectile1].velocity.Y = -randomSpeedY;
				    Main.projectile[projectile2].velocity.X = randomSpeedX;
				    Main.projectile[projectile2].velocity.Y = -randomSpeedY;
				}
			}
		}
		
		public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.Next(4) == 0)
	        {
	            int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 244);
	        }
	    }
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.BreakerBlade);
			recipe.AddIngredient(ItemID.HellstoneBar, 10);
			recipe.AddIngredient(null, "EssenceofCinder");
			recipe.AddIngredient(ItemID.SoulofMight, 3);
			recipe.AddIngredient(ItemID.SoulofSight, 3);
			recipe.AddIngredient(ItemID.SoulofFright, 3);
			recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	}
}
