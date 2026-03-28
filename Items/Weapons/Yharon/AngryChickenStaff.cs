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
using Terraria.Audio;

namespace CalamityModClassicPreTrailer.Items.Weapons.Yharon
{
    public class AngryChickenStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Yharon's Kindle Staff");
            /* Tooltip.SetDefault("Summons the Son of Yharon to fight for you\n" +
                               "The dragon increases your life regen, defense, and movement speed while summoned\n" +
                               "Requires 4 minion slots to use"); */
        }

        public override void SetDefaults()
        {
            Item.mana = 50;
            Item.damage = 160;
            Item.useStyle = 1;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.noMelee = true;
            Item.knockBack = 7f;
            Item.value = Item.buyPrice(1, 80, 0, 0);
            Item.rare = 10;
            Item.UseSound = new SoundStyle("CalamityModClassicPreTrailer/Sounds/Item/FlareSound");
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("AngryChicken").Type;
            Item.shootSpeed = 10f;
            Item.DamageType = DamageClass.Summon;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 14;
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