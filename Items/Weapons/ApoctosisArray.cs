using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.Weapons {
public class ApoctosisArray : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/ApoctosisArray");
        return true;
    }


	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Apoctosis Array");
		Item.width = 98;  //The width of the .png file in pixels divided by 2.
		Item.damage = 90;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Magic;  //Dictates whether this is a melee-class weapon.
		Item.mana = 6;
		Item.useAnimation = 7;
		Item.useTime = 7;  //Ranges from 1 to 55. 
		Item.useStyle = 5;
		Item.knockBack = 6.75f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item91;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.noMelee = true;
		Item.height = 34;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		////Tooltip.SetDefault("Fires ion blasts that speed up and then explode\nThe higher your mana the more damage they will do");
		Item.value = 5000000;  //Value is calculated in copper coins.
		Item.rare = 9;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("IonBlast").Type;
		Item.shootSpeed = 6f;
	}
	
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-25, 0);
	}
	
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
        float manaAmount = ((float)player.statMana * 0.02f);
        float damageMult = manaAmount;
        int projectile = Projectile.NewProjectile(source, position, velocity,type, (int)((double)damage * damageMult), knockback, player.whoAmI, 0.0f, 0.0f);
        Main.projectile[projectile].scale = (manaAmount * 0.375f);
    	return false;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "IonBlaster");
		recipe.AddIngredient(ItemID.LunarBar, 10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}
