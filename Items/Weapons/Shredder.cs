using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
	public class Shredder : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Shredder");
			/* Tooltip.SetDefault("The myth, the legend, the weapon that drops more frames than any other\n" +
				"Fires a barrage of energy bolts that split and bounce\n" +
				"Right click to fire a barrage of normal bullets"); */
		}

	    public override void SetDefaults()
	    {
			Item.damage = 25;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 56;
			Item.height = 24;
			Item.useTime = 3;
			Item.reuseDelay = 10;
			Item.useAnimation = 24;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 1.5f;
            Item.value = Item.buyPrice(1, 20, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item31;
			Item.autoReuse = true;
			Item.shoot = 10;
			Item.shootSpeed = 12f;
			Item.useAmmo = 97;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
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
			int bulletAmt = 4;
			if (player.altFunctionUse == 2)
    		{
			    for (int index = 0; index < bulletAmt; ++index)
			    {
			        float num7 = velocity.X;
			        float num8 = velocity.Y;
			        float SpeedX = velocity.X + (float) Main.rand.Next(-30, 31) * 0.05f;
			        float SpeedY = velocity.Y + (float) Main.rand.Next(-30, 31) * 0.05f;
			        int shot = Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0f, 0f);
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
			        float SpeedX = velocity.X + (float) Main.rand.Next(-30, 31) * 0.05f;
			        float SpeedY = velocity.Y + (float) Main.rand.Next(-30, 31) * 0.05f;
			        int shot = Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, Mod.Find<ModProjectile>("ChargedBlast").Type, damage, knockback, player.whoAmI, 0f, 0f);
                    Main.projectile[shot].timeLeft = 180;
                }
			    return false;
			}
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "GalacticaSingularity", 5);
            recipe.AddIngredient(null, "BarofLife", 5);
            recipe.AddIngredient(null, "ChargedDartRifle");
            recipe.AddIngredient(ItemID.LunarBar, 5);
            recipe.AddIngredient(ItemID.ClockworkAssaultRifle);
            recipe.AddIngredient(ItemID.Shotgun);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
		}
	}
}