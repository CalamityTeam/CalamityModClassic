using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.IO;
using Terraria.ObjectData;
using Terraria.Utilities;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.AbyssWeapons
{
    public class CalamarisLament : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Calamari's Lament");
            // Tooltip.SetDefault("Summons a squid to fight for you");
        }

        public override void SetDefaults()
        {
            Item.damage = 158;
            Item.mana = 10;
            Item.width = 62;
            Item.height = 62;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.knockBack = 2.5f;
            Item.value = Item.buyPrice(1, 40, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item83;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("Calamari").Type;
            Item.shootSpeed = 10f;
            Item.DamageType = DamageClass.Summon;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse != 2)
			{
				position = Main.MouseWorld;
				velocity.X = 0;
				velocity.Y = 0;
				Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);
			}
			return false;
		}

		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			if (player.altFunctionUse == 2)
			{
				player.MinionNPCTargetAim(true);
			}
			return base.UseItem(player);
		}
	}
}