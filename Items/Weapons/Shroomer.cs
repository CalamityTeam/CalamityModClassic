using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Shroomer : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/Shroomer");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Shroomer");
        Item.damage = 200;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 96;
        Item.height = 40;
        ////Tooltip.SetDefault("Fires bullets at a high velocity\nChance to fire an extremely powerful homing shroom when shot");
        Item.crit += 35;
        Item.useTime = 26;
        Item.useAnimation = 26;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 9.75f;
        Item.value = 900000;
        Item.rare = 9;
        Item.UseSound = SoundID.Item40;
        Item.autoReuse = true;
        Item.shoot = 10;
        Item.shootSpeed = 10f;
        Item.useAmmo = 97;
    }
    
    public override Vector2? HoldoutOffset()
	{
		return new Vector2(-25, 0);
	}
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	Projectile.NewProjectile(source, position, velocity,type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
    	if (Main.rand.Next(5) == 0)
    	{
    		Projectile.NewProjectile(source, position, velocity,Mod.Find<ModProjectile>("Shroom").Type, (int)((double)damage * 1.5f), knockback, player.whoAmI, 0.0f, 0.0f);
    	}
    	return false;
	}

    public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.SniperRifle);
        recipe.AddIngredient(ItemID.ShroomiteBar, 11);
        recipe.AddIngredient(ItemID.FragmentVortex, 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}