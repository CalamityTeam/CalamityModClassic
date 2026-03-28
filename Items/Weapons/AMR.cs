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
    public class AMR : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Anti-materiel Rifle");
            /* Tooltip.SetDefault("Fires a .50 caliber sniper round that rips apart enemy defense and DR\n" +
                "If you crit the target a second swarm of bullets will fire near the target"); */
        }

        public override void SetDefaults()
        {
            Item.damage = 7000;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 76;
            Item.crit += 20;
            Item.height = 30;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 9.5f;
            Item.value = Item.buyPrice(1, 80, 0, 0);
            Item.rare = 10;
            Item.UseSound = new SoundStyle("CalamityModClassicPreTrailer/Sounds/Item/PlasmaBlast");
            Item.autoReuse = true;
            Item.shoot = 10;
            Item.shootSpeed = 12f;
            Item.useAmmo = 97;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 14;
		}

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Shroomer");
            recipe.AddIngredient(null, "CosmiliteBar", 10);
            recipe.AddTile(null, "DraedonsForge");
            recipe.Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("AMR").Type, damage, knockback, player.whoAmI, 0f, 0f);
            return false;
        }
	}
}