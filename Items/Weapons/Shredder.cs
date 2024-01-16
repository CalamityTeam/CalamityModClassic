using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons
{
	public class Shredder : ModItem
	{
		public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	    {
		    //texture =("CalamityModClassic1Point1/Items/Weapons/Shredder");
		    return true;
	    }
	
	    public override void SetDefaults()
	    {
		    //Tooltip.SetDefault("Shredder");
			Item.damage = 24;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 56;
			Item.height = 24;
			////Tooltip.SetDefault("Consumes four ammo per shot\nThe myth, the legend, the weapon that drops more frames than any other");
			Item.useTime = 3;
			Item.reuseDelay = 14;
			Item.useAnimation = 12;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 1.5f;
			Item.value = 1000000;
			Item.rare = 10;
			Item.UseSound = SoundID.Item31;
			Item.autoReuse = true;
			Item.shoot = 10; //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 6.5f;
			Item.useAmmo = 97;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{    
		    int num6 = Main.rand.Next(15, 20);
		    for (int index = 0; index < num6; ++index)
		    {
		        float num7 = velocity.X;
		        float num8 = velocity.Y;
		        float SpeedX = velocity.X + (float) Main.rand.Next(-80, 81) * 0.05f;
		        float SpeedY = velocity.Y + (float) Main.rand.Next(-80, 81) * 0.05f;
		        Projectile.NewProjectile(source, position, new Vector2(SpeedX, SpeedY),type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
		    }
		    return false;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "GalacticaSingularity", 5);
            recipe.AddIngredient(null, "BarofLife", 5);
            recipe.AddIngredient(ItemID.LunarBar, 5);
            recipe.AddIngredient(ItemID.ClockworkAssaultRifle);
            recipe.AddIngredient(ItemID.Shotgun);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
		}
	}
}