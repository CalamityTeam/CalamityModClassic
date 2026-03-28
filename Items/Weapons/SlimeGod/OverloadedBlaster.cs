using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.SlimeGod
{
	public class OverloadedBlaster : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Overloaded Blaster");
			// Tooltip.SetDefault("33% chance to not consume gel");
		}

	    public override void SetDefaults()
	    {
			Item.damage = 16;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 42;
			Item.height = 20;
			Item.useTime = 17;
			Item.useAnimation = 17;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 1.5f;
            Item.value = Item.buyPrice(0, 12, 0, 0);
            Item.rare = 4;
			Item.UseSound = SoundID.Item9;
			Item.autoReuse = true;
			Item.shootSpeed = 6.5f;
			Item.shoot = Mod.Find<ModProjectile>("SlimeBolt").Type;
			Item.useAmmo = 23;
		}
	    
	    public override bool CanConsumeAmmo(Item ammo, Player player)
	    {
	    	if (Main.rand.Next(0, 100) < 33)
	    		return false;
	    	return true;
	    }
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			float SpeedA = velocity.X;
            float SpeedB = velocity.Y;
            int num6 = Main.rand.Next(5, 6);
            for (int index = 0; index < num6; ++index)
            {
            	float num7 = velocity.X;
                float num8 = velocity.Y;
                float SpeedX = velocity.X + (float) Main.rand.Next(-40, 41) * 0.05f;
                float SpeedY = velocity.Y + (float) Main.rand.Next(-40, 41) * 0.05f;
                Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
            }
            return false;
		}
	}
}