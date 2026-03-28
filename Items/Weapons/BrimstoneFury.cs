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
	public class BrimstoneFury : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Brimstone Fury");
		}

		public override void SetDefaults()
		{
			Item.damage = 21;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 30;
			Item.height = 58;
			Item.useTime = 22;
			Item.useAnimation = 22;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 3.75f;
			Item.value = Item.buyPrice(0, 48, 0, 0);
			Item.rare = 6;
			Item.UseSound = SoundID.Item5;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("BrimstoneBolt").Type;
			Item.shootSpeed = 13f;
			Item.useAmmo = 40;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			int numProj = 2;
			float rotation = MathHelper.ToRadians(3);
			for (int i = 0; i < numProj + 1; i++)
			{
				Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numProj - 1)));
				Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, Mod.Find<ModProjectile>("BrimstoneBolt").Type, damage, knockback, player.whoAmI, 0f, 0f);
			}
			return false;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "UnholyCore", 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}