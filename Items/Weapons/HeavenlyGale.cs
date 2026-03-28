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
	public class HeavenlyGale : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Heavenly Gale");
			/* Tooltip.SetDefault("Fires a barrage of 5 random exo arrows\n" +
                "Green exo arrows explode into a tornado on death\n" +
                "Blue exo arrows cause a second group of arrows to fire on enemy hits\n" +
                "Orange exo arrows cause explosions on death\n" +
                "Teal exo arrows ignore enemy immunity frames"); */
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 700;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 44;
	        Item.height = 58;
	        Item.useTime = 9;
	        Item.useAnimation = 18;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 4f;
            Item.value = Item.buyPrice(2, 50, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item5;
	        Item.autoReuse = true;
	        Item.shoot = 1;
	        Item.shootSpeed = 17f;
	        Item.useAmmo = 40;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 15;
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
	    	float num117 = 0.314159274f;
			int num118 = 5;
			Vector2 vector7 = new Vector2(velocity.X, velocity.Y);
			vector7.Normalize();
			vector7 *= 40f;
			bool flag11 = Collision.CanHit(vector2, 0, 0, vector2 + vector7, 0, 0);
			for (int num119 = 0; num119 < num118; num119++)
			{
				float num120 = (float)num119 - ((float)num118 - 1f) / 2f;
				Vector2 value9 = vector7.RotatedBy((double)(num117 * num120), default(Vector2));
				if (!flag11)
				{
					value9 -= vector7;
				}
				switch (Main.rand.Next(10))
				{
                    case 0:
		    		case 1:
                    case 2:
                    case 3: type = Mod.Find<ModProjectile>("TealExoArrow").Type; break;
                    case 4:
                    case 5:
                    case 6: type = Mod.Find<ModProjectile>("OrangeExoArrow").Type; break;
                    case 7:
                    case 8: type = Mod.Find<ModProjectile>("BlueExoArrow").Type; break;
                    case 9: type = Mod.Find<ModProjectile>("GreenExoArrow").Type; break;
				}
				Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X + value9.X, vector2.Y + value9.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
			}
			return false;
	    }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Alluvion");
            recipe.AddIngredient(null, "AstrealDefeat");
            recipe.AddIngredient(null, "ClockworkBow");
            recipe.AddIngredient(null, "FlarewingBow");
            recipe.AddIngredient(null, "Phangasm");
            recipe.AddIngredient(null, "PlanetaryAnnihilation");
            recipe.AddIngredient(null, "TheBallista");
            recipe.AddIngredient(null, "NightmareFuel", 5);
            recipe.AddIngredient(null, "EndothermicEnergy", 5);
            recipe.AddIngredient(null, "CosmiliteBar", 5);
            recipe.AddIngredient(null, "DarksunFragment", 5);
            recipe.AddIngredient(null, "HellcasterFragment", 3);
            recipe.AddIngredient(null, "Phantoplasm", 5);
            recipe.AddIngredient(null, "AuricOre", 25);
            recipe.AddTile(null, "DraedonsForge");
            recipe.Register();
        }
    }
}