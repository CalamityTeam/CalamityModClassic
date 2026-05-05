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
	public class FulgurationHalberd : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fulguration Halberd");
			/* Tooltip.SetDefault("Inflicts burning blood on enemy hits\n" +
				"Right click to change modes"); */
			ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
		}

		public override void SetDefaults()
		{
			Item.width = 60;
			Item.damage = 53;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 22;
			Item.useStyle = 1;
			Item.useTime = 22;
			Item.useTurn = true;
			Item.knockBack = 5f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 62;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
			Item.shoot = Mod.Find<ModProjectile>("NobodyKnows").Type;
			Item.shootSpeed = 6f;
			ItemID.Sets.ItemsThatAllowRepeatedRightClick[Item.type] = true;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				Item.noMelee = true;
				Item.noUseGraphic = true;
				Item.useStyle = 5;
				Item.autoReuse = false;
			}
			else
			{
				Item.noMelee = false;
				Item.noUseGraphic = false;
				Item.useStyle = 1;
				Item.autoReuse = true;
			}
			return base.CanUseItem(player);
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse == 2)
			{
				Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("FulgurationHalberd").Type, damage, knockback, player.whoAmI, 0f, 0f);
			}
			return false;
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(Mod.Find<ModBuff>("BurningBlood").Type, 300);
		}

		public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.CrystalShard, 20);
	        recipe.AddIngredient(ItemID.OrichalcumHalberd);
	        recipe.AddIngredient(ItemID.HellstoneBar, 10);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	        recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.CrystalShard, 20);
	        recipe.AddIngredient(ItemID.MythrilHalberd);
	        recipe.AddIngredient(ItemID.HellstoneBar, 10);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}
