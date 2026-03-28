using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;
using Terraria.Audio;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
	public class OpalStriker : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Opal Striker");
			// Tooltip.SetDefault("Fires a string of opal strikes");
		}

	    public override void SetDefaults()
	    {
			Item.damage = 9;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 64;
			Item.height = 16;
			Item.useTime = 5;
			Item.reuseDelay = 25;
			Item.useAnimation = 20;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 0f;
            Item.value = Item.buyPrice(0, 2, 0, 0);
            Item.rare = 2;
			Item.UseSound = new SoundStyle("CalamityModClassicPreTrailer/Sounds/Item/OpalStrike");
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("OpalStrike").Type;
			Item.shootSpeed = 6f;
			Item.useAmmo = 97;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
		    Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("OpalStrike").Type, damage, 0f, player.whoAmI, 0.0f, 0.0f);
		    return false;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Marble, 20);
            recipe.AddIngredient(ItemID.Amber, 5);
            recipe.AddIngredient(ItemID.Diamond, 3);
            recipe.AddIngredient(ItemID.FlintlockPistol);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
		}
	}
}