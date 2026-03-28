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
	public class Genisis : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Genisis");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 48;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 4;
	        Item.width = 74;
	        Item.height = 28;
	        Item.useTime = 3;
	        Item.useAnimation = 3;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 1.5f;
            Item.value = Item.buyPrice(1, 20, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item33;
	        Item.autoReuse = true;
	        Item.shootSpeed = 6f;
	        Item.shoot = Mod.Find<ModProjectile>("BigBeamofDeath").Type;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, 0);
        }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	    {
	    	Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
	        int num6 = 3;
	        float SpeedX = velocity.X + (float) Main.rand.Next(-20, 21) * 0.05f;
		    float SpeedY = velocity.Y + (float) Main.rand.Next(-20, 21) * 0.05f;
	        for (int index = 0; index < num6; ++index)
	        {
	            int projectile = Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, SpeedX * 1.05f, SpeedY * 1.05f, 440, (int)((double)damage * 0.75f), knockback, player.whoAmI, 0.0f, 0.0f);
	            Main.projectile[projectile].timeLeft = 120;
	        }
	        return false;
		}
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.LaserMachinegun);
	        recipe.AddIngredient(ItemID.LunarBar, 5);
	        recipe.AddIngredient(null, "BarofLife", 5);
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
	    }
	}
}