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
	public class AuguroftheElements : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Augur of the Elements");
			// Tooltip.SetDefault("Casts a burst of elemental tentacles to spear your enemies");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 74;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 6;
	        Item.width = 28;
	        Item.crit = 3;
	        Item.height = 30;
	        Item.useTime = 1;
	        Item.reuseDelay = 10;
	        Item.useAnimation = 10;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 5.5f;
            Item.value = Item.buyPrice(1, 20, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item103;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("ElementTentacle").Type;
	        Item.shootSpeed = 30f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "EldritchTome");
			recipe.AddIngredient(null, "TomeofFates");
			recipe.AddIngredient(ItemID.ShadowFlameHexDoll);
			recipe.AddIngredient(null, "GalacticaSingularity", 5);
			recipe.AddIngredient(ItemID.LunarBar, 5);
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
	    	float num78 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
			float num79 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
	    	Vector2 value2 = new Vector2(num78, num79);
			value2.Normalize();
			Vector2 value3 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
			value3.Normalize();
			value2 = value2 * 6f + value3;
			value2.Normalize();
			value2 *= Item.shootSpeed;
			float num91 = (float)Main.rand.Next(10, 50) * 0.001f;
			if (Main.rand.Next(2) == 0)
			{
				num91 *= -1f;
			}
			float num92 = (float)Main.rand.Next(10, 50) * 0.001f;
			if (Main.rand.Next(2) == 0)
			{
				num92 *= -1f;
			}
			Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, value2.X, value2.Y, type, damage, knockback, player.whoAmI, num92, num91);
	    	return false;
		}
	}
}