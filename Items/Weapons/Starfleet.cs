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
	public class Starfleet : ModItem
	{
		public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	    {
		    //texture =("CalamityModClassic1Point1/Items/Weapons/Starfleet");
		    return true;
	    }
	
	    public override void SetDefaults()
	    {
		    //Tooltip.SetDefault("Starfleet");
			Item.damage = 250;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 80;
			Item.height = 26;
			////Tooltip.SetDefault("Consumes fallen stars\nBLOWS UP EVERYTHING!!!");
			Item.useTime = 40;
			Item.useAnimation = 40;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 15f;
			Item.value = 1000000;
			Item.rare = 9;
			Item.UseSound = SoundID.Item92;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("PlasmaBlast").Type; //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 12f;
			Item.useAmmo = 75;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{    
		    int num6 = Main.rand.Next(5, 6);
		    for (int index = 0; index < num6; ++index)
		    {
		        float num7 = velocity.X;
		        float num8 = velocity.Y;
		        float SpeedX = velocity.X + (float) Main.rand.Next(-40, 41) * 0.05f;
		        float SpeedY = velocity.Y + (float) Main.rand.Next(-40, 41) * 0.05f;
		        Projectile.NewProjectile(source, position, new Vector2(SpeedX, SpeedY),type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
		    }
		    return false;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.ElectrosphereLauncher);
            recipe.AddIngredient(ItemID.FragmentStardust, 20);
            recipe.AddIngredient(ItemID.StarCannon);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
		}
	}
}