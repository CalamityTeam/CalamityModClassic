using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items.Weapons
{
	public class OverloadedBlaster : ModItem
	{
		public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	    {
		    //texture = "CalamityModClassic1Point0/Items/Weapons/OverloadedBlaster");
		    return true;
	    }
	
	    public override void SetDefaults()
	    {
		    //DisplayName.SetDefault("Overloaded Blaster");
			Item.damage = 30;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 42;
			Item.height = 20;
			//Tooltip.SetDefault("Fires a storm of magic slime at enemies");
			Item.useTime = 12;
			Item.useAnimation = 12;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 1.5f;
			Item.value = 100000;
			Item.rare = 5;
			Item.UseSound = SoundID.Item9;
			Item.autoReuse = true;
			Item.shoot = 10; //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 6.5f;
			Item.shoot = Mod.Find<ModProjectile>("SlimeBolt").Type;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			float speedX = velocity.X;
			float speedY = velocity.Y;
			float SpeedA = speedX;
            float SpeedB = speedY;
            int num6 = Main.rand.Next(5, 6);
            for (int index = 0; index < num6; ++index)
            {
            	float num7 = speedX;
                float num8 = speedY;
                float SpeedX = speedX + (float) Main.rand.Next(-40, 41) * 0.05f;
                float SpeedY = speedY + (float) Main.rand.Next(-40, 41) * 0.05f;
                Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
            }
            return false;
		}
	}
}