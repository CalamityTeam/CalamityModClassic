using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class AbyssalWarhammer : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/AbyssalWarhammer");
        return true;
    }
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Abyssal Warhammer");
        Item.damage = 42;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.width = 62;
        Item.height = 58;
        Item.useTime = 38;
        Item.useAnimation = 38;
        Item.useTurn = true;
        Item.hammer = 110;
        Item.useStyle = 1;
        Item.knockBack = 8;
        Item.value = 550000;
        Item.rare = 5;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "VerstaltiteBar", 8);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if(Main.rand.Next(5) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 29);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
		target.AddBuff(BuffID.Frostburn, 100);
	}
}}