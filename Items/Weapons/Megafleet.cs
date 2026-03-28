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
	public class Megafleet : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Voidragon");
		}

	    public override void SetDefaults()
	    {
			Item.damage = 1000;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 96;
			Item.height = 38;
			Item.useTime = 5;
			Item.useAnimation = 5;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 5f;
            Item.value = Item.buyPrice(5, 0, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;
			Item.shoot = 10;
			Item.shootSpeed = 18f;
			Item.useAmmo = 97;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 16;
		}

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, 0);
        }
	    
	    public override bool CanConsumeAmmo(Item ammo, Player player)
	    {
	    	if (Main.rand.Next(0, 100) < 95)
	    		return false;
	    	return true;
	    }
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
		    float SpeedX = velocity.X + (float) Main.rand.Next(-5, 6) * 0.05f;
		    float SpeedY = velocity.Y + (float) Main.rand.Next(-5, 6) * 0.05f;
            type = (Main.rand.Next(2) == 0 ? Mod.Find<ModProjectile>("Voidragon").Type : type);
		    Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
		    return false;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "Seadragon");
            recipe.AddIngredient(null, "ShadowspecBar", 5);
            recipe.AddIngredient(ItemID.SoulofMight, 30);
            recipe.AddTile(null, "DraedonsForge");
            recipe.Register();
		}
	}
}