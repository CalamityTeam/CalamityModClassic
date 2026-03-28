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
    public class AethersWhisper : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Aether's Whisper");
            /* Tooltip.SetDefault("Inflicts several long-lasting debuffs and splits on tile hits\n" +
                "Right click to change from magic to ranged damage"); */
        }

        public override void SetDefaults()
        {
            Item.damage = 1050;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 30;
            Item.width = 118;
            Item.height = 38;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 5.5f;
            Item.value = Item.buyPrice(1, 40, 0, 0);
            Item.rare = 10;
            Item.UseSound = new SoundStyle("CalamityModClassicPreTrailer/Sounds/Item/LaserCannon");
            Item.autoReuse = true;
            Item.shootSpeed = 12f;
            Item.shoot = Mod.Find<ModProjectile>("AetherBeam").Type;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.DamageType = DamageClass.Ranged;
                Item.mana = 0;
            }
            else
            {
                Item.DamageType = DamageClass.Magic;
                Item.mana = 30;
            }
            return base.CanUseItem(player);
        }

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			float ai0 = (player.altFunctionUse == 2 ? 1f : 0f);
			Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, ai0, 0f);
			return false;
		}

		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "PlasmaRod");
            recipe.AddIngredient(null, "Zapper");
            recipe.AddIngredient(null, "SpectreRifle");
            recipe.AddIngredient(null, "TwistingNether", 3);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}