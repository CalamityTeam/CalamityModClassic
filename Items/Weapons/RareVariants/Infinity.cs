using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.RareVariants
{
	public class Infinity : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Infinity");
			/* Tooltip.SetDefault("Bad PC\n" +
				"Fires a barrage of energy bolts that split and bounce\n" +
				"Right click to fire a barrage of normal bullets"); */
		}

	    public override void SetDefaults()
	    {
			Item.damage = 30;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 56;
			Item.height = 24;
			Item.useTime = 2;
			Item.reuseDelay = 6;
			Item.useAnimation = 18000;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 1f;
            Item.value = Item.buyPrice(1, 20, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item31;
			Item.autoReuse = true;
			Item.shoot = 10;
			Item.shootSpeed = 12f;
			Item.useAmmo = 97;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 22;
		}
	    
	    public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
	    
	    public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			int bulletAmt = 2;
			if (player.altFunctionUse == 2)
    		{
			    for (int index = 0; index < bulletAmt; ++index)
			    {
			        float num7 = velocity.X;
			        float num8 = velocity.Y;
			        float SpeedX = velocity.X + (float) Main.rand.Next(-15, 16) * 0.05f;
			        float SpeedY = velocity.Y + (float) Main.rand.Next(-15, 16) * 0.05f;
			        int shot = Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, 0f, 0f);
                    Main.projectile[shot].timeLeft = 180;
                }
			    return false;
			}
			else
			{
			    for (int index = 0; index < bulletAmt; ++index)
			    {
			        float num7 = velocity.X;
			        float num8 = velocity.Y;
			        float SpeedX = velocity.X + (float) Main.rand.Next(-15, 16) * 0.05f;
			        float SpeedY = velocity.Y + (float) Main.rand.Next(-15, 16) * 0.05f;
			        int shot = Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("ChargedBlast").Type, damage, knockback, player.whoAmI, 0f, 0f);
                    Main.projectile[shot].timeLeft = 180;
                }
			    return false;
			}
		}
	}
}