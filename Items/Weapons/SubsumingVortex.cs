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
	public class SubsumingVortex : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Subsuming Vortex");
			// Tooltip.SetDefault("Fires 3 vortexes of elemental energy");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 520;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 20;
	        Item.width = 28;
	        Item.height = 30;
	        Item.UseSound = SoundID.Item84;
	        Item.useTime = 20;
	        Item.useAnimation = 20;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 5f;
            Item.value = Item.buyPrice(2, 50, 0, 0);
            Item.rare = 10;
            Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("Vortex").Type;
	        Item.shootSpeed = 9f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 15;
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{    
		    int num6 = 3;
		    for (int index = 0; index < num6; ++index)
		    {
		        float SpeedX = velocity.X + (float) Main.rand.Next(-50, 51) * 0.05f;
		        float SpeedY = velocity.Y + (float) Main.rand.Next(-50, 51) * 0.05f;
		        float ai = (Main.rand.NextFloat() + 0.5f);
		        Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, ai);
		    }
		    return false;
		}
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "AuguroftheElements");
	        recipe.AddIngredient(null, "NuclearFury");
	        recipe.AddIngredient(null, "RelicofRuin");
	        recipe.AddIngredient(null, "TearsofHeaven");
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