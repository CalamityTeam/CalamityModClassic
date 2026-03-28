using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
	public class StarfleetMK2 : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Starmada");
		}

	    public override void SetDefaults()
	    {
            Item.damage = 308;
            Item.knockBack = 15f;
            Item.shootSpeed = 16f;
            Item.useStyle = 5;
            Item.useAnimation = 27;
            Item.useTime = 27;
            Item.reuseDelay = 0;
            Item.width = 122;
            Item.height = 50;
            Item.UseSound = SoundID.Item92;
            Item.shoot = Mod.Find<ModProjectile>("StarfleetMK2").Type;
            Item.value = Item.buyPrice(1, 80, 0, 0);
            Item.rare = 10;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.DamageType = DamageClass.Ranged;
            Item.channel = true;
            Item.useTurn = false;
            Item.useAmmo = 75;
            Item.autoReuse = true;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 14;
		}

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("StarfleetMK2").Type, 0, 0f, player.whoAmI);
            return false;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Starfleet");
            recipe.AddIngredient(null, "CosmiliteBar", 10);
            recipe.AddIngredient(null, "ExodiumClusterOre", 15);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
		}
	}
}