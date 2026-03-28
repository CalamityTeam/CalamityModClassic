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
	public class Photoviscerator : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Photoviscerator");
			/* Tooltip.SetDefault("90% chance to not consume gel\n" +
                "Fires a stream of exo flames that literally melts everything"); */
		}

	    public override void SetDefaults()
	    {
			Item.damage = 165;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 84;
			Item.height = 30;
			Item.useTime = 1;
			Item.useAnimation = 5;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 2f;
			Item.UseSound = SoundID.Item34;
            Item.value = Item.buyPrice(2, 50, 0, 0);
            Item.rare = 10;
            Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("ExoFire").Type;
			Item.shootSpeed = 6f;
			Item.useAmmo = 23;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 15;
		}

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-15, 0);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float SpeedX = velocity.X + (float)Main.rand.Next(-5, 6) * 0.05f;
            float SpeedY = velocity.Y + (float)Main.rand.Next(-5, 6) * 0.05f;
            Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0f, 0f);
            return false;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
	    {
	    	if (Main.rand.Next(0, 100) < 90)
	    		return false;
	    	return true;
	    }
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "ElementalEruption");
	        recipe.AddIngredient(null, "CleansingBlaze");
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