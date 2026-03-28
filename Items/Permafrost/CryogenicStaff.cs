using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Permafrost
{
	public class CryogenicStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cryogenic Staff");
			/* Tooltip.SetDefault(@"Summons an animated ice construct to protect you
Fire rate and range increase the longer it targets an enemy"); */
		}

		public override void SetDefaults()
		{
			Item.damage = 56;
            Item.mana = 10;
			Item.DamageType = DamageClass.Summon;
            Item.sentry = true;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = 1;
			Item.noMelee = true;
			Item.knockBack = 4f;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
			Item.UseSound = SoundID.Item78;
			Item.shoot = Mod.Find<ModProjectile>("IceSentry").Type;
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
                Projectile.NewProjectile(Entity.GetSource_FromThis(null),position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);
                player.UpdateMaxTurrets();
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
