using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Patreon
{
    public class LightGodsBrilliance : ModItem
    {
		
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Light God's Brilliance");
            // Tooltip.SetDefault("Casts small, homing light beads along with explosive light balls");
        }

        public override void SetDefaults()
        {
            Item.damage = 100;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 4;
            Item.width = 34;
            Item.height = 36;
            Item.useTime = 3;
            Item.useAnimation = 3;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 3f;
            Item.value = Item.buyPrice(1, 80, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item9;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("LightBead").Type;
            Item.shootSpeed = 25f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 21;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			int num6 = Main.rand.Next(2, 5);
			for (int index = 0; index < num6; ++index)
			{
				float num7 = velocity.X;
				float num8 = velocity.Y;
				float SpeedX = velocity.X + (float)Main.rand.Next(-50, 51) * 0.05f;
				float SpeedY = velocity.Y + (float)Main.rand.Next(-50, 51) * 0.05f;
				Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, SpeedX, SpeedY, type,
					(int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
			}
			if (Main.rand.Next(3) == 0)
			{
				Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("LightBall").Type, (int)((double)damage * 2.0), knockback, player.whoAmI, 0.0f, 0.0f);
			}
			
			return false;
		}
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "ShadecrystalTome");
			recipe.AddIngredient(null, "AbyssalTome");
			recipe.AddIngredient(ItemID.HolyWater, 10);
			recipe.AddIngredient(null, "EndothermicEnergy", 5);
			recipe.AddIngredient(null, "NightmareFuel", 5);
			recipe.AddIngredient(ItemID.SoulofLight, 30);
			recipe.AddIngredient(null, "EffulgentFeather", 5);
			recipe.AddTile(TileID.Bookcases);
			recipe.Register();
		}
	}
}