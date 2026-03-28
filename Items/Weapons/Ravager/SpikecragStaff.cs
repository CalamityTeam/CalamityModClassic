using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.Ravager
{
	public class SpikecragStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Spikecrag Staff");
			// Tooltip.SetDefault("Summons an spikecrag to protect you");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 120;
			Item.mana = 10;
	        Item.DamageType = DamageClass.Summon;
			Item.sentry = true;
	        Item.width = 50;
	        Item.height = 50;
	        Item.useTime = 30;
	        Item.useAnimation = 30;
	        Item.useStyle = 1;
	        Item.noMelee = true;
	        Item.knockBack = 2f;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.rare = 8;
	        Item.autoReuse = true;
	        Item.shootSpeed = 20f;
	        Item.UseSound = SoundID.Item78;
			Item.shoot = Mod.Find<ModProjectile>("Spikecrag").Type;
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