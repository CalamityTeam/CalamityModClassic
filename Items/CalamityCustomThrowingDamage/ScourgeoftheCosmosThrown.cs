using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage
{
	public class ScourgeoftheCosmosThrown : CalamityDamageItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Scourge of the Cosmos");
		}

		public override void SafeSetDefaults()
		{
			Item.width = 18;
			Item.damage = 1500;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 20;
			Item.useStyle = 5;
			Item.useTime = 20;
			Item.knockBack = 5f;
			Item.UseSound = SoundID.Item109;
			Item.autoReuse = true;
			Item.height = 20;
            Item.value = Item.buyPrice(1, 80, 0, 0);
            Item.rare = 10;
            Item.shoot = Mod.Find<ModProjectile>("ScourgeoftheCosmos").Type;
			Item.shootSpeed = 15f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 14;
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, 0f, 1f);
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ScourgeoftheCorruptor);
            recipe.AddIngredient(null, "CosmiliteBar", 10);
            recipe.AddIngredient(null, "DarksunFragment", 10);
            recipe.AddIngredient(null, "XerocPitchfork", 200);
            recipe.AddTile(null, "DraedonsForge");
            recipe.Register();
        }
    }
}
