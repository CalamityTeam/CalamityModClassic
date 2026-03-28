using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;
using CalamityModClassicPreTrailer.Projectiles;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
	public class StarCannonEX : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Star Cannon EX");
		}

	    public override void SetDefaults()
	    {
			Item.damage = 88;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 66;
			Item.height = 22;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = 5;
            Item.rare = 7;
			Item.noMelee = true;
			Item.knockBack = 8f;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.UseSound = SoundID.Item9;
			Item.autoReuse = true;
			Item.shoot = 12;
			Item.shootSpeed = 15f;
			Item.useAmmo = AmmoID.FallenStar;
		}

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, 0);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{    
		    int num6 = Main.rand.Next(1, 3);
		    for (int index = 0; index < num6; ++index)
		    {
		        float num7 = velocity.X;
		        float num8 = velocity.Y;
		        float SpeedX = velocity.X + (float) Main.rand.Next(-15, 16) * 0.05f;
		        float SpeedY = velocity.Y + (float) Main.rand.Next(-15, 16) * 0.05f;
                switch (Main.rand.Next(3))
                {
                    case 0: break;
                    case 1: type = 9; break;
                    case 2: type = Mod.Find<ModProjectile>("AstralStar").Type; break;
                }
		        int star = Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0f, 0f);
				Main.projectile[star].GetGlobalProjectile<CalamityGlobalProjectile>().forceRanged = true;
			}
		    return false;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.StarCannon);
            recipe.AddIngredient(null, "AstralBar", 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
		}
	}
}